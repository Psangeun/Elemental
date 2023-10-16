using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	GameManager gameManager;
	GameObject key;

	public int characterNumber = 0;

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

	Vector3 movement;
	bool isJumping = false;
	int jumpCount = 1;
	float timer = 0;

	public bool haveKey = false;

	GameObject[] emotes = new GameObject[3];

	ObjectManager objectManager;
	string obstacleObj;

	// 0. ¹°À½Ç¥, 1. ´À³¦Ç¥, 3. Á×À½

	void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
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
		if (characterNumber != 2 && Input.GetButtonDown("Jump") && jumpCount == 1)
		{
			isJumping = true;
			jumpCount--;
			animator.SetTrigger("JumpTrigger");
			animator.SetBool("isJumping", true);
		}

		if(characterNumber == 0 && gameManager.isDoorOpen && Input.GetKeyDown(KeyCode.UpArrow))
        {
			gameObject.SetActive(false);
			gameManager.clear = true;
		}

		if(characterNumber == 0 && haveKey && Input.GetKeyDown(KeyCode.C))
        {
			Vector2 newKeyPosition = gameObject.transform.position;

			Transform keyTransform = key.GetComponent<Transform>();

			keyTransform.position = newKeyPosition;

			key.SetActive(true);

			haveKey = false;
			gameManager.waterHaveKey = false;

			emotes[1].SetActive(true);
			audioSource.PlayOneShot(keyDropClip);
			Invoke("EmoteOff", 1f);
		}


		if(SceneManager.GetActiveScene().name.Contains("Devil"))
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

		if (characterNumber == 1)
        {
			if (Input.GetKeyDown(KeyCode.X))
            {
				
				if (timer > 1.5f)
				{
					timer = 0;
					
					GameObject skillObstacle = objectManager.MakeObj(obstacleObj);
					skillObstacle.transform.position = gameObject.transform.position;
				}
			}
		}

        if (characterNumber == 2)
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
		if(!gameManager.gameOver)
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
			if (characterNumber == 0)
            {
				haveKey = true;
				gameManager.waterHaveKey = true;
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

		if(collision.gameObject.tag == "door")
        {
			if(haveKey)
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
			gameManager.gameOver = true;
			animator.SetBool("isDie", true);

			StartCoroutine(DieEmote());
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "button")
        {
			if(characterNumber != 3)
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
		while (gameManager.gameOver)
		{
			emotes[2].SetActive(true);
			yield return new WaitForSeconds(0.5f);
			emotes[2].SetActive(false);
			yield return new WaitForSeconds(0.5f);
		}
	}
}