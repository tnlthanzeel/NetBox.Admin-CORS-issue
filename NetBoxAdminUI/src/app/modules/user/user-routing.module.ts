import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { UserManagementComponent } from "./user-management.component";
import { UserProfileComponent } from "./user-profile/user-profile.component";
const routes: Routes = [
    {
        path: '',
        component: UserManagementComponent,
    },
    {
        path: 'user-profile',
        component: UserProfileComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class UserRoutingModule { }
