using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    bool isLeft;
    bool isRight;

    bool isHit;
    bool isCatch;

    Vector3 movePos = new Vector3(0.01f, 0, 0);
    Vector3 downMove = new Vector3(0, 0.01f, 0);

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isLeft && transform.position.x > -6.6f)
        {
            transform.position -= movePos;
        }

        if (isRight && transform.position.x < 6.6f)
        {
            transform.position += movePos;
        }
    }

    // 左を押したら左に移動(-6.6fまで移動できる)
    public void LeftButtonDOWN()
    {
        isLeft = true;
    }

    // 左を離したら移動が止まる
    public void LeftButtonUP()
    {
        isLeft = false;
    }

    // 右を押したら右に移動(6.6fまで移動できる)
    public void RightButtonDOWN()
    {
        isRight = true;
    }

    // 右を離したら移動が止まる
    public void RightButtonUP()
    {
        isRight = false;
    }

    public void CatchButton()
    {
        if (!isCatch)
        {
            StartCoroutine(CatchMove());
        }
    }


    IEnumerator CatchMove()
    {
        isCatch = true;

        // キャッチを押したら何かにぶつかるまで下に下がる
        while (!isHit)
        {
            transform.position -= downMove;
            yield return null;
        }

        yield return null;

        // キャッチのアニメーションをする
        animator.SetBool("isCatch", true);

        yield return new WaitForSeconds(1f);

        // 上にあがる
        while (transform.position.y < 2.5f)
        {
            transform.position += downMove;
            yield return null;
        }

        // 初期位置に戻る
        while (transform.position.x > -6.6f)
        {
            transform.position -= movePos;
            yield return null;
        }

        // リリースアニメーションをする
        animator.SetBool("isCatch", false);

        yield return new WaitForSeconds(1f);
        isCatch = false;
        isHit = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "hitObj")
        {
            isHit = true;
        }
    }
}
