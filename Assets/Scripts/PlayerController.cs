using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	GameObject key;

	public int characterNumber = 0;

	private int WATER = 0;
	private int FIRE = 1;
	private int AIR = 2;
	private int EARTH = 3;

	public Sprite[] slimeSprites = new Sprite[4];
	public RuntimeAnimatorController[] slimeAnim = new RuntimeAnimatorController[4];

	private AudioSource audioSource;
	public AudioClip jumpClip;
	public AudioClip emoteClip;
	public AudioClip keyPickClip;
	public AudioClip keyDropClip;

	public float movePower = 3f;
	public float jumpPower = 7f;

	Rigidbody2D rigid;
	SpriteRenderer rend;
	Animator animator;

	bool isJumping = false;
	int jumpCount = 1;
	float timer = 0;

	public bool haveKey = false;

	GameObject[] emotes = new GameObject[3]; // 0. 물음표, 1. 느낌표, 2. gameover

	ObjectManager objectManager;
	string obstacleObj;

	void Awake()
	{
		objectManager = FindObjectOfType<ObjectManager>();

		for (int i = 0; i < emotes.Length; i++)
		{
			emotes[i] = gameObject.transform.GetChild(i).gameObject;
		}

		obstacleObj = "skill";
	}
	void Start()
	{
		this.audioSource = this.gameObject.AddComponent<AudioSource>();
		this.audioSource.clip = this.jumpClip;
		this.audioSource.loop = false;

		key = GameObject.Find("Key");

		rigid = gameObject.GetComponent<Rigidbody2D>();
        rend = gameObject.GetComponentInChildren<SpriteRenderer>();
		animator = gameObject.GetComponentInChildren<Animator>();
	}

	
	void Update()
	{
		if(GameManager.Instance.isGameover)
        {
			return;
        }

		if (characterNumber != AIR && Input.GetButtonDown("Jump") && jumpCount == 1)
		{
			isJumping = true;
			jumpCount--;
			animator.SetTrigger("JumpTrigger");
			animator.SetBool("isJumping", true);
		}

		if(characterNumber == WATER && GameManager.Instance.isDoorOpen && Input.GetKeyDown(KeyCode.UpArrow))
        {
			gameObject.SetActive(false);
			GameManager.Instance.clear = true;
		}

		if (characterNumber == WATER && haveKey && Input.GetKeyDown(KeyCode.C))
		{
			if (!SceneManager.GetActiveScene().name.Contains("Devil"))
			{
				return;
			}

			Vector2 newKeyPosition = gameObject.transform.position;

			Transform keyTransform = key.GetComponent<Transform>();

			keyTransform.position = newKeyPosition;

			key.SetActive(true);

			haveKey = false;
			GameManager.Instance.waterHaveKey = false;

			emotes[1].SetActive(true);
			audioSource.PlayOneShot(keyDropClip);
			Invoke("EmoteOff", 1f);
		}

		if (SceneManager.GetActiveScene().name.Contains("Devil"))
        {
			if (Input.GetKeyDown(KeyCode.C))
			{
				characterNumber++;
				characterNumber %= 4;

				SpriteRenderer playerSpriteRenderer = GetComponent<SpriteRenderer>();
				playerSpriteRenderer.sprite = slimeSprites[characterNumber];

				Animator playerAnimator = GetComponent<Animator>();
                playerAnimator.runtimeAnimatorController = slimeAnim[characterNumber];
            }
		}
		timer += Time.deltaTime;

		
		if (Input.GetKeyDown(KeyCode.X))
		{
			if(characterNumber != FIRE)
            {
				emotes[0].SetActive(true);
				audioSource.PlayOneShot(emoteClip);
				Invoke("EmoteOff", 1f);
			}
			else
            {
				if (timer > 0.5f)
				{
					timer = 0;

					GameObject skillObstacle = objectManager.MakeObj(obstacleObj);
					skillObstacle.transform.position = gameObject.transform.position;
				}
			}
		}

		if (characterNumber == AIR)
        {
			rigid.gravityScale = -1;
			rend.flipY = true;
		}
        else
        {
			rigid.gravityScale = 1;
			rend.flipY = false;
		}
    }

	void FixedUpdate()
	{
		if(!GameManager.Instance.isGameover)
        {
			Move();
			Jump();
		}
	}

    void LateUpdate()
    {
		LimitToMove();
    }
    void Move()
	{
		Vector3 moveVelocity = Vector3.zero;

		if (Input.GetAxisRaw("Horizontal") < 0)
		{
			moveVelocity = Vector3.left;
			rend.flipX = true;
		}

		else if (Input.GetAxisRaw("Horizontal") > 0)
		{
			moveVelocity = Vector3.right;
			rend.flipX = false;
		}

		transform.position += moveVelocity * movePower * Time.deltaTime;
	}

	void Jump()
	{
		if (!isJumping)
			return;

		this.GetComponent<AudioSource>().Play();

		rigid.velocity = Vector2.zero;

		Vector2 jumpVelocity = new Vector2(0, jumpPower);
		rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

		isJumping = false;
	}

	void LimitToMove()
    {
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10.0f, 29.0f), Mathf.Clamp(transform.position.y, -6.0f, 15f));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.layer == 8 && rigid.velocity.y < 0)
        {
			animator.SetBool("isJumping", false);
			jumpCount = 1;
		}

		if (collision.gameObject.tag == "key")
		{
			if (characterNumber == WATER)
			{
				haveKey = true;
				GameManager.Instance.waterHaveKey = true;
				audioSource.PlayOneShot(keyPickClip);
				animator.SetBool("haveKey", true);
			}
			else
			{
				emotes[0].SetActive(true);
				audioSource.PlayOneShot(emoteClip);
				Invoke("EmoteOff", 1f);
			}
		}

		if (collision.gameObject.tag == "door")
		{
			if (haveKey)
			{
				animator.SetBool("haveKey", false);
			}
			else
			{
				emotes[0].SetActive(true);
				audioSource.PlayOneShot(emoteClip);
				Invoke("EmoteOff", 1f);
			}
		}

		if (collision.gameObject.tag == "obstacle" || collision.gameObject.tag == "plant")
		{
			GameManager.Instance.isGameover = true;
			StartCoroutine(GameManager.Instance.GameOver());
			animator.SetBool("isDie", true);

			emotes[2].SetActive(true);
			//StartCoroutine(DieEmote());
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "button")
        {
			if(characterNumber != EARTH)
            {
				emotes[0].SetActive(true);
				audioSource.PlayOneShot(emoteClip);
				Invoke("EmoteOff", 1f);
			}
		}
    }
    void EmoteOff()
    {
		for(int i=0; i<emotes.Length; i++)
        {
			emotes[i].SetActive(false);
		}
	}

	IEnumerator DieEmote()
	{
		while (GameManager.Instance.isGameover)
		{
			emotes[2].SetActive(true);
			yield return new WaitForSeconds(0.5f);
			emotes[2].SetActive(false);
			yield return new WaitForSeconds(0.5f);
		}
	}
}