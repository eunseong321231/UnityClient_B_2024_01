using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharacterDifferent : ExCharacter
{
    // Start is called before the first frame update


    protected override void Move()
        {
            base.Move();
            transform.Translate(
                Vector3.up * speed * Time.deltaTime);
        }
 }

