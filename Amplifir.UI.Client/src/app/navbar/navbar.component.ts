import { Component, OnInit, Output, EventEmitter } from '@angular/core';

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

  @Output() clicked = new EventEmitter<string>();

  ngOnInit() {
    this.click(this.navLinks[0].label);
  }

  click(linkLabel: string) {
    this.clicked.emit(linkLabel);
  }

}
