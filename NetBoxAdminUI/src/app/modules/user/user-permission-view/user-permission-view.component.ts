import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { MaterialModule } from '../../shared/material.module';
import { SharedModule } from '../../shared/shared.module';
import { CommonModule } from '@angular/common';
import { ErrorResponse } from '../../../core/models/error-response.model';
import { ResponseResult } from '../../../core/models/response-result.model';
import { ToasterService } from '../../../core/services/toaster.service';
import { UpdateUserRoleClaimModel } from '../models/update-user-role-claims.model';
import { PermissionModel, PermissionListModel, PermissionGroup } from '../models/user-permission.model';
import { UserRoleModel, UserClaimModel } from '../models/user-role.model';
import { UserPermissionService } from '../services/user-permission.service';

@Component({
  selector: 'netbox-user-permission-view',
  templateUrl: './user-permission-view.component.html',
  styleUrl: './user-permission-view.component.scss',
  standalone: true,
  imports: [CommonModule, SharedModule, MaterialModule]
})

export class UserPermissionViewComponent implements OnInit {

  userRole: UserRoleModel;
  displayStyle = 'none';
  isAllPermissionChecked = false;
  permissionGroups: PermissionGroup[];
  permissionNames = new Array<string>();
  updatePermissionModel = new UpdateUserRoleClaimModel();

  @Input() roleId: string = '';
  @Input() isPermissionSelection: boolean;
  @Input() isView: boolean;
  @Input() isEdit: boolean;
  @Input() isPermissionEdit: boolean;
  @Input() selectedPermission = new Array<string>();
  @Input() retrievedPermission = new Array<UserClaimModel>();
  @Output() selectedPermissionNames = new EventEmitter<string[]>();
  @Output() isPopupClosed = new EventEmitter<boolean>();

  constructor(private toasterService: ToasterService, private userPermissionService: UserPermissionService) {

  }

  loadPermissions(roleId: string, isPermissionEdit: boolean, isPermissionSelection: boolean, retrievedPermissions: UserClaimModel[], selectedPermissions: string[]) {
    this.roleId = roleId;
    this.isPermissionEdit = isPermissionEdit;
    this.isPermissionSelection = isPermissionSelection;
    this.retrievedPermission = retrievedPermissions;
    this.selectedPermission = selectedPermissions;

    if ((this.roleId != '' && this.roleId != undefined) && !this.isPermissionEdit) {
      this.getUserRolePermissions(this.roleId);
    } else if (this.roleId != '' && this.isPermissionEdit) {
      this.getUserRoleByUser();
    }

    if (this.isPermissionSelection) {
      this.getSelectedTemplates();
    }

    if (this.retrievedPermission.length != 0) {
      this.getUserRoleByUser();
    }
  }

  ngOnInit(): void {
    this.getUserPermissions();
  }

  makeAllPermissionFalse() {
    this.permissionGroups.forEach((pg) => {
      pg.value.forEach(f => {
        f.isAllPermission = false;
      });
    });

    this.permissionNames = [];
    this.selectedPermissionNames.emit(this.permissionNames);
  }

  getUserPermissions() {
    this.userPermissionService.getAll().subscribe({
      next: (permissionGroups: ResponseResult<PermissionGroup[]>) => {
        this.permissionGroups = permissionGroups.data;
        if (this.userRole != undefined)
          this.getUserRolePermissions(this.userRole.roleId);
      },
      error: (err: ErrorResponse) => {
        this.toasterService.error(err);
      },
    });
  }

  getUserRoleByUser() {
    this.permissionNames = [];
    this.selectedPermission = [];
    this.retrievedPermission.forEach((userClaim) =>
      this.permissionNames.push(userClaim.claimValue)
    );
    this.selectedPermission.push(...this.permissionNames);
    this.selectedPermissionNames.emit(this.permissionNames);
    this.getSelectedTemplates();
  }

  getSelectedTemplates() {
    this.permissionGroups.forEach((p) => {
      p.value.forEach((permissionModel) => {
        permissionModel.value.forEach((f) => (f.isSelected = false));
        this.selectedPermission.forEach((permissionName) => {
          let selectedPermiss = permissionModel.value.find(
            (f) => f.key == permissionName
          );
          if (selectedPermiss != undefined) {
            permissionModel.value.find(
              (f) => f.key == permissionName
            )!.isSelected = true;
          }
        });
      });
    });
  }

  getUserRolePermissions(roleId: string) {
    this.retrievedPermission = [];
    this.userPermissionService.getPermissionsByRole(roleId).subscribe({
      next: (userPermissions: ResponseResult<UserRoleModel>) => {
        this.permissionNames = [];

        this.permissionGroups.forEach((pg) => {
          pg.value.forEach((f) => {
            userPermissions.data.claims.forEach((e) => {
              let selectedPermission = f.value.find(
                (sp) => sp.key == e.claimValue
              );
              if (selectedPermission != undefined) {
                f.value.find((sp) => sp.key == e.claimValue)!.isSelected = true;
                this.permissionNames.push(selectedPermission.key);
                this.retrievedPermission.push(e);
              }
            });
          });
        });
        this.selectedPermission.push(...this.permissionNames);
        this.selectedPermissionNames.emit(this.permissionNames);
      },
      error: (err: ErrorResponse) => {
        this.toasterService.error(err);
      },
    });
  }

  selectedPermissions(
    event: any,
    permissionListModel: PermissionListModel,
    permissionKey: string
  ) {

    let checked = event.target.checked;

    if (checked) {
      this.permissionNames.push(permissionListModel.key);
      var permissionGroup = this.permissionGroups.find(s => s.key == 'Admin');
      var permission = permissionGroup?.value.filter(k => k.key == permissionKey)!;

      permission.map((permissionModel: PermissionModel) => {

        let permissionContainCount = 0;
        this.permissionNames.forEach(element => {
          if (element.split('.')[0].toLowerCase().startsWith(permissionKey.toLowerCase().replace(' ', ''))) {
            permissionContainCount += 1
          }
          else {
            permissionModel.isAllPermission = false;
          }
        });

        if (permissionContainCount == permissionModel.value.length) {
          permissionModel.isAllPermission = true;
        }
        else {
          permissionModel.isAllPermission = false;
        }
      })
      this.selectedPermissionNames.emit(this.permissionNames);

    } else {
      const index = this.permissionNames.indexOf(permissionListModel.key);
      this.permissionNames.splice(index, 1);

      var permissionGroup = this.permissionGroups.find(s => s.key == 'Admin');
      var permission = permissionGroup?.value.filter(k => k.key == permissionKey)!;

      permission.map((permissionModel: PermissionModel) => {
        let permissionContainCount = 0;

        this.permissionNames.forEach(element => {
          if (element.split('.')[0].toLowerCase().startsWith(permissionKey.toLowerCase().replace(' ', ''))) {
            permissionContainCount += 1
          }
          else {
            permissionModel.isAllPermission = false;
          }
        });

        if (permissionContainCount == permissionModel.value.length) {
          permissionModel.isAllPermission = true;
        }
        else {
          permissionModel.isAllPermission = false;
        }
      })

      this.selectedPermissionNames.emit(this.permissionNames);
    }
  }

  changePermissionList(event: any, permissionModelName: string) {
    let checked = event.target.checked;

    var permissionGroup = this.permissionGroups.find(s => s.key == 'Admin')!;

    const permissionModelList = permissionGroup.value.find(
      (f) => f.key == permissionModelName
    );

    permissionModelList?.value.forEach((f) => {
      if (checked) {
        this.permissionNames.push(f.key);
        this.selectedPermissionNames.emit(this.permissionNames);
      } else {
        const index = this.permissionNames.indexOf(f.key);
        this.permissionNames.splice(index, 1);
        this.selectedPermissionNames.emit(this.permissionNames);
      }
    });

    permissionModelList!.isAllPermission = checked;
  }
}

