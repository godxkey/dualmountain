using System;
using UnityEngine;
using DualMountain.WorldBusiness.Facades;

namespace DualMountain.WorldBusiness.Controller {

    public class RoleController {

        public RoleController() {}

        public void Init(Transform root) {

            RoleEntity role = root.GetComponentInChildren<RoleEntity>();
            Debug.Assert(role != null);

            if (AllWorldRepo.RoleEntity != null) {
                Debug.LogError("WHY? 不应该有角色存在");
            } else {
                AllWorldRepo.SetRoleEntity(role);
                Debug.Log("找到角色");
            }

            // 让相机看向角色
            var cm = AllWorldRepo.CameraEntity;
            cm.Follow(role.transform);

        }

        public void Tick(float deltaTime) {

            var cam = AllWorldRepo.CameraEntity;
            var player = AllWorldRepo.PlayerEntity;
            if (cam == null || player == null) {
                return;
            } 

            cam.RotateHorizontal(player.camRotateHorizontal);
            cam.RotateVertical(player.camRotateVertical);
            cam.PullDistance(player.pullDistance);

        }

        public void FixedTick(float fixedDeltaTime) {

            var role = AllWorldRepo.RoleEntity;
            if (role == null) {
                return;
            }

            var player = AllWorldRepo.PlayerEntity;

            // 移动它
            Camera camera = AllWorldRepo.Camera;
            Vector2 moveAxis = player.moveAxis;
            role.Move(camera.transform, moveAxis);

            // 跳
            float jumpAxis = player.jumpAxis;
            if (jumpAxis > 0) {
                role.Jump(jumpAxis);
                player.jumpAxis = 0;
            }

            // 下落
            role.Falling(fixedDeltaTime);

            // 落地检测
            role.CheckGround(fixedDeltaTime);

        }

    }

}