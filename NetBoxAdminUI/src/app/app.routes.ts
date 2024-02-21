import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout/layout.component';
import { LoginComponent } from './modules/auth/login/login.component';
import { NgModule } from '@angular/core';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    {
        path: 'admin',
        component: LayoutComponent,
        canActivate: [authGuard],
        children: [
            {
                path: 'users',
                loadChildren: () => import('./modules/user/user.module').then(t => t.UserModule)
            },
            {
                path: 'settings',
                loadChildren: () => import('./modules/settings/settings.module').then(t => t.SettingsModule)
            }
        ]
    },
    {
        path: '',
        redirectTo: 'admin',
        pathMatch: 'full'
    },
    {
        path: 'login',
        component: LoginComponent
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
