import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FooterComponent } from './footer/footer.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LayoutComponent } from './layout/layout.component';
import { AuthorizationDirective } from '../modules/shared/directives/authorization.directive';
import { MaterialModule } from '../modules/shared/material.module';


@NgModule({
  declarations: [FooterComponent, NavbarComponent, SidenavComponent, LayoutComponent],
  imports: [
    CommonModule,
    RouterModule,
    NgbModule,
    AuthorizationDirective,
    MaterialModule
  ]
})
export class LayoutModule { }
