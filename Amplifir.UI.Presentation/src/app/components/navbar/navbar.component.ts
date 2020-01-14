/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

import { Component, OnInit, Output, EventEmitter, AfterViewInit } from '@angular/core';

import { RouterLinkViewModel } from '../../viewModels/routerLinkViewModel';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements AfterViewInit {

  constructor() { }

  navLinks: RouterLinkViewModel[] = [
    new RouterLinkViewModel( 'Feed', '/feed' ),
    new RouterLinkViewModel( 'Profile', '/profile' ),
    new RouterLinkViewModel( 'Settings', '/settings' )
  ]

  @Output() clicked = new EventEmitter<string>();

  ngAfterViewInit(): void {
    // TODO: Instead of using localStorage, change the title from a route event.
    // TEMPORARY.
    const linkLabel = localStorage.getItem( 'linkLabel' );

    if (linkLabel) {
      setTimeout(() => {
        ( document.querySelectorAll( `a.nav-link.${linkLabel}` )[0] as HTMLAnchorElement ).click();
      });

    } else {
      this.click( this.navLinks[0].label );
    }
  }

  click(linkLabel: string) {
    localStorage.setItem( 'linkLabel', linkLabel );
    this.clicked.emit(linkLabel);
  }

}
