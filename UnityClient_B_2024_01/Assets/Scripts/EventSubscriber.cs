using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    public EventChannel eventChannel;

    private void OnEnable()     //������Ʈ�� Ȱ��ȭ �Ǿ��� �� �̺�Ʈ ���
    {
        eventChannel.OnEventRaised.AddListener(OnEventRaised);
    }

    private void OnDisable()     //������Ʈ�� ��Ȱ��ȭ �Ǿ��� �� �̺�Ʈ ����
    {
        eventChannel.OnEventRaised.RemoveListener(OnEventRaised);
    }

    void OnEventRaised()
    {
        Debug.Log("Event On" + gameObject.name);
    }
}