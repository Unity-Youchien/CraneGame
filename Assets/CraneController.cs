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

    // �����������獶�Ɉړ�(-6.6f�܂ňړ��ł���)
    public void LeftButtonDOWN()
    {
        isLeft = true;
    }

    // ���𗣂�����ړ����~�܂�
    public void LeftButtonUP()
    {
        isLeft = false;
    }

    // �E����������E�Ɉړ�(6.6f�܂ňړ��ł���)
    public void RightButtonDOWN()
    {
        isRight = true;
    }

    // �E�𗣂�����ړ����~�܂�
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

        // �L���b�`���������牽���ɂԂ���܂ŉ��ɉ�����
        while (!isHit)
        {
            transform.position -= downMove;
            yield return null;
        }

        yield return null;

        // �L���b�`�̃A�j���[�V����������
        animator.SetBool("isCatch", true);

        yield return new WaitForSeconds(1f);

        // ��ɂ�����
        while (transform.position.y < 2.5f)
        {
            transform.position += downMove;
            yield return null;
        }

        // �����ʒu�ɖ߂�
        while (transform.position.x > -6.6f)
        {
            transform.position -= movePos;
            yield return null;
        }

        // �����[�X�A�j���[�V����������
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
