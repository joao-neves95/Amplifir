import { Component, OnInit } from '@angular/core';

import { RouterLinkViewModel } from "../viewModels/routerLinkViewModel";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor() { }

  navLinks: RouterLinkViewModel[] = [
    new RouterLinkViewModel( 'Feed', '/feed' ),
    new RouterLinkViewModel( 'Profile', '/profile' ),
    new RouterLinkViewModel( 'Settings', '/settings' )
  ]

  ngOnInit() {
  }

}
