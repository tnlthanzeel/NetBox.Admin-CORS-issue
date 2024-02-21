import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppPermissions } from '../../core/extenstion/permissions';

@Component({
  selector: 'netbox-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrl: './sidenav.component.scss'
})
export class SidenavComponent {
  menuItems: any[];
  isCollapsed = true;
  panelOpenState = false;
  p = AppPermissions;

  constructor(private router: Router) { }

  ngOnInit() {
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
    });
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['login']);
  }

  openSubMenu() {

  }
}
