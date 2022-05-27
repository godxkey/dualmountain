using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DualMountain.WorldBusiness.Facades;

namespace DualMountain.WorldBusiness.Controller {

    public class InputController {

        public InputController() {}

        public void Init() {

            // 绑定输入
            var input = AllWorldRepo.InputEntity;
            input.OnMoveHandle += OnMove;
            input.OnJumpHandle += OnJump;

        }

        public void Tick(float deltaTime) {

            var player = AllWorldRepo.PlayerEntity;
            if (player == null) {
                return;
            }

            // TODO: 这是临时的
            var mouse = Mouse.current;
            float mouseHorizontal = mouse.delta.x.ReadValue();
            float mouseVertical = mouse.delta.y.ReadValue();
            Vector2 mouseScroll = mouse.scroll.ReadValue();
            player.camRotateHorizontal = mouseHorizontal * deltaTime;
            player.camRotateVertical = mouseVertical * deltaTime;
            player.pullDistance = -mouseScroll.y * deltaTime;

        }

        void OnMove(Vector2 moveAxis) {
            // 获取玩家
            var player = AllWorldRepo.PlayerEntity;
            player.moveAxis = moveAxis;
        }

        void OnJump(float jumpAxis) {
            var player = AllWorldRepo.PlayerEntity;
            player.jumpAxis = jumpAxis;
        }

    }

}