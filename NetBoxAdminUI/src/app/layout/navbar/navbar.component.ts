import { Component, ElementRef, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'netbox-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {
  public focus: any;
  public listTitles: any[];
  public location: Location;
  constructor(location: Location, private element: ElementRef, private router: Router) {
    this.location = location;
  }

  ngOnInit() {
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['login']);
  }
}