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

import {
  Constants
} from '../../../constants';
import {
  Shout,
  FilterType,
  ShoutsService,
  ApiResponseOfListOfShout,
  ApiException
} from '../../../services/apiClient.service';

@Component( {
  templateUrl: './profile.component.html',
  styleUrls: [ './profile.component.scss' ]
} )
export class ProfileComponent implements OnInit {

  constructor( private shoutsService: ShoutsService ) {}

  readonly CONSTANTS = Constants;

  userName: string = Constants.defaultLabels.userName;
  bio: string = '';
  location: string = '';
  birthDate: string = '';
  website: string = '';
  followingCount: number = 0;
  followersCount: number = 0;

  shouts: Shout[] = [];

  ngOnInit() {
    this.nextPage();
  }

  nextPage() {
    this.shoutsService.get( FilterType.Top, this.shouts.length > 0 ? this.shouts[this.shouts.length - 1].id : 0, 15 )
      .subscribe( res => {
        this.shouts = ( < ApiResponseOfListOfShout > res ).endpointResult || this.shouts;

      }, err => {
        // TODO: Create a global generic method for error handling.
        // TODO: Create a global generic method to get ApiException the serialized responses.
        const errRes: ApiResponseOfListOfShout = ApiResponseOfListOfShout.fromJS( JSON.parse( ( < ApiException > err ).response ) );
        console.log( "ERROR IN shoutsService.get() API CALL (res):", ( < ApiException > err ).response );
        // TODO: Create an alert popup (sweetalert2).
        alert( errRes.message );
      } );
  }

}
