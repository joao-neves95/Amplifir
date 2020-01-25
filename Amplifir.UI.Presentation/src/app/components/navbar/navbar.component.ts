/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

import {
  Component,
  OnInit,
  Output,
  EventEmitter
} from '@angular/core';
import {
  Router,
  NavigationEnd
} from '@angular/router';

import {
  RouterLinkViewModel
} from '../../viewModels/routerLinkViewModel';

@Component( {
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: [ './navbar.component.scss' ]
} )
export class NavbarComponent implements OnInit {

  constructor( private router: Router ) {}

  navLinks: RouterLinkViewModel[] = [
    new RouterLinkViewModel( 'Explore', '/explore', 'fi fi-hashtag' ),
    new RouterLinkViewModel( 'Feed', '/feed', 'fi fi-podcast' /* 'fi fi-earth' */ ),
    new RouterLinkViewModel( 'Profile', '/profile', 'fi fi-person' ),
    new RouterLinkViewModel( 'Settings', '/settings', 'fi fi-player-settings' )
  ]

  @Output() pageChange = new EventEmitter < string > ();

  ngOnInit(): void {
    this.pageChange.emit( this.navLinks[ 0 ].label );

    this.router.events.subscribe( ( navEvent ) => {
      if ( navEvent instanceof NavigationEnd ) {
        const navLink = this.navLinks.find( ( link ) => link.url === navEvent.url );

        if ( navLink ) {
          this.pageChange.emit( navLink.label );
        }
      }
    } );
  }
}
