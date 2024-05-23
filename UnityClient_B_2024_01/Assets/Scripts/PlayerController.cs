using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public NPCManager npcManager;
    public GameStateManager gameStateManager;
    private CharacterController characterContoller;
    private Vector3 moveDirection;
    public float range = 2.0f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = transform.TransformDirection(new Vector3(horizontalInput, 0, verticalInput));
        moveDirection = move * moveSpeed;

        characterContoller.Move(moveDirection * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            foreach (Collider collider in colliders)
                if(collider.CompareTag("NPC"))
                {
                    Entity_dialog.Param npcParam =
                        npcManager.GetParamData(collider.GetComponent<NPCActor>().npcNumber, gameStateManager.gameState);

                    if(npcParam != null)
                    {
                        Debug.Log($"Dialog : {npcParam.Dialog}");

                        if(npcParam.changeState > 0)
                        {
                            gameStateManager.gameState = npcParam.changeState;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("해당하는 데이터가 없습니다. ");
                    }
                }
        }
    }
}
