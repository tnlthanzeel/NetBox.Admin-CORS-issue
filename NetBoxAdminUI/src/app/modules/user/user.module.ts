import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { UserManagementComponent } from './user-management.component';
import { UserCreateComponent } from './user-create/user-create.component';
import { UserViewComponent } from './user-view/user-view.component';
import { UserRoleCreateComponent } from './user-role-create/user-role-create.component';
import { UserRoleViewComponent } from './user-role-view/user-role-view.component';
import { UserPermissionViewComponent } from './user-permission-view/user-permission-view.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserRoutingModule } from './user-routing.module';


@NgModule({
  declarations: [UserManagementComponent, UserCreateComponent, UserViewComponent, UserRoleCreateComponent, UserRoleViewComponent, UserProfileComponent],
  imports: [
    CommonModule,
    UserRoutingModule,
    SharedModule,
    UserPermissionViewComponent,
  ]
})
export class UserModule { }
