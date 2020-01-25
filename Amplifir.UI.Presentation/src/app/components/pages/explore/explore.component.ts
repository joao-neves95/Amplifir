/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

import {
  Component,
  OnInit
} from '@angular/core';

import { Shout, ShoutsService, FilterType, ApiResponseOfListOfShout, ApiException } from '../../../services/apiClient.service';

@Component( {
  selector: 'app-explore',
  templateUrl: './explore.component.html',
  styleUrls: [ './explore.component.scss' ]
} )
export class ExploreComponent implements OnInit {

  constructor(private shoutsService: ShoutsService) {}

  shouts: Shout[] = [];

  ngOnInit() {
    this.nextPage();
  }

  nextPage() {
    this.shoutsService.get( FilterType.Top, this.shouts.length > 0 ? this.shouts[ this.shouts.length - 1 ].id : 0, 15 )
      .subscribe( res => {
        this.shouts = ( res as ApiResponseOfListOfShout ).endpointResult || this.shouts;

      }, err => {
        const errRes: ApiResponseOfListOfShout = ApiResponseOfListOfShout.fromJS( JSON.parse( ( err as ApiException ).response ) );
        console.log( 'ERROR IN shoutsService.get() API CALL (res):', ( err as ApiException ).response );
        alert( errRes.message );
      } );
  }

}
