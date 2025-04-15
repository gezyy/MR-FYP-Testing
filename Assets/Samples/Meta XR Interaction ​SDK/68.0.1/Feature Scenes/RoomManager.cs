using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Meta.XR.Util;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Meta.XR.MRUtilityKit
{
    public class RoomManager : MonoBehaviour
    {
        // RoomCreatedEvent 事件，供其他脚本调用
        public delegate void RoomCreatedDelegate(MRUKRoom room);
        public event RoomCreatedDelegate RoomCreatedEvent;

        private void OnEnable()
        {
            // 订阅事件
            RoomCreatedEvent += AddRoomToRoomEnv;
        }

        private void OnDisable()
        {
            // 取消订阅事件
            RoomCreatedEvent -= AddRoomToRoomEnv;
        }

        // 事件处理方法：将房间添加到 RoomEnv
        private void AddRoomToRoomEnv(MRUKRoom room)
        {
            // 获取场景中名为 "RoomEnv" 的物体
            GameObject roomEnv = GameObject.Find("RoomEnv");

            // 如果不存在 RoomEnv，创建一个新的
            if (roomEnv == null)
            {
                roomEnv = new GameObject("RoomEnv");
            }

            // 将房间对象设置为 RoomEnv 的子物体
            room.gameObject.transform.SetParent(roomEnv.transform);
        }

        // 你可以在这里手动触发事件，用于调试或其他逻辑
        public void TriggerRoomCreatedEvent(MRUKRoom room)
        {
            RoomCreatedEvent?.Invoke(room);
        }
    }
}