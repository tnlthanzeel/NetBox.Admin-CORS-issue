import { Component, OnInit, ViewChild } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AppPermissions } from '../../../core/extenstion/permissions';
import { ErrorResponse } from '../../../core/models/error-response.model';
import { ResponseResult } from '../../../core/models/response-result.model';
import { SearchRequestModel } from '../../../core/models/search-request.model';
import { ToasterService } from '../../../core/services/toaster.service';
import { UserRoleModel } from '../models/user-role.model';
import { UserPermissionService } from '../services/user-permission.service';
import { UserRoleService } from '../services/user-role.service';
import { UserPermissionViewComponent } from '../user-permission-view/user-permission-view.component';
import { isEmpty } from '../../../core/extenstion/helper';
import { UserRoleCreateComponent } from '../user-role-create/user-role-create.component';
import { ModalService } from '../../../core/services/modal.service';
import { ConfirmationModalComponent } from '../../shared/components/confirmation-modal/confirmation-modal.component';

@Component({
  selector: 'netbox-user-role-view',
  templateUrl: './user-role-view.component.html',
  styleUrl: './user-role-view.component.scss'
})
export class UserRoleViewComponent implements OnInit {

  userRoles: UserRoleModel[];
  isBlocked = false;
  isEdit = false;
  roleId: string;
  displayStyle = "none";
  focus: any;

  searchModel = new SearchRequestModel(10, 1);
  resultsEmpty$ = new BehaviorSubject<boolean>(false);
  p = AppPermissions;
  @ViewChild('userPermissionComponent') userPermissionComponent: UserPermissionViewComponent;
  @ViewChild('userRoleCreateComponent') userRoleCreateComponent: UserRoleCreateComponent;

  constructor(
    private userRoleService: UserRoleService,
    private userPermissionService: UserPermissionService,
    private toasterService: ToasterService, private modalService: ModalService) { }

  ngOnInit(): void {
    this.getUserRoles();
  }

  openPopup() {
    this.userPermissionComponent.getUserPermissions();
    this.displayStyle = "block";
  }

  closePopup() {
    this.displayStyle = "none";
  }

  editRole(roleId: string) {
    this.isEdit = true;
    this.roleId = roleId;
  }

  deleteRole(roleId: string) {
    let options = {
      title: 'Delete User Role',
      message: 'Are you sure you want to delete the user role ?',
    };
    this.modalService.displayDialog(ConfirmationModalComponent, options)
    this.modalService.confirmed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.isBlocked = true;
        this.userRoleService.deleteRole(roleId)
          .subscribe({
            next: (res: any) => {
              this.isBlocked = false;
              this.toasterService.success("User role has been successfully deleted");
              this.getUserRoles();
            },
            error: (err: ErrorResponse) => {
              this.isBlocked = false;
              this.toasterService.error(err);
            }
          })
      }
    });
  }

  getUserRoles() {
    this.isBlocked = true;
    this.userRoleService.getAll(this.searchModel).subscribe({
      next: (roles: ResponseResult<UserRoleModel[]>) => {
        this.userRoles = roles.data;
        if (!roles.success || !roles.totalRecordCount) {
          this.resultsEmpty$.next(true)
          this.isBlocked = false;
          return
        } else {
          this.resultsEmpty$.next(false)
        }
        this.isBlocked = false;
        this.isEdit = false
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.toasterService.error(err);
      }
    });
  }

  search() {
    if (!isEmpty(this.searchModel.searchTerm) && this.searchModel.searchTerm.length > 2)
      this.getUserRoles();
    else if (isEmpty(this.searchModel.searchTerm)) {
      this.getUserRoles();
    }
  }

  clearSearchTerm() {
    this.searchModel.searchTerm = '';
    this.search();
  }

  viewPermission(userRole: UserRoleModel) {
    this.userPermissionComponent.isView = true;
    this.userPermissionComponent.isPermissionEdit = false;
    this.userPermissionComponent.userRole = userRole;
    this.openPopup();
  }

  viewCreateRole() {
    this.userRoleCreateComponent.openPopup();
  }

  viewEditRole() {
    this.userRoleCreateComponent.openPopup();
  }
}
