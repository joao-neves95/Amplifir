/*
 * Copyright (c) 2019 - 2020 Jo�o Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

import { Component, OnInit } from '@angular/core';

import { Constants } from '../../../constants';
import { AuthService, UserCredentialsDTO } from '../../../services/apiClient.service';

@Component({
  selector: 'app-auth-modal',
  templateUrl: './auth-modal.component.html',
  styleUrls: ['./auth-modal.component.scss']
})
export class AuthModalComponent implements OnInit {

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
  }

  register() {
    const userCredentialsDTO: UserCredentialsDTO = new UserCredentialsDTO();
    userCredentialsDTO.email = '';
    userCredentialsDTO.password = '';

    this.authService.register( userCredentialsDTO )
      .subscribe(
        res => '',
        err => ''
      );

    // this.saveJWT( '' );
  }

  login() {
    const userCredentialsDTO: UserCredentialsDTO = new UserCredentialsDTO();
    userCredentialsDTO.email = '';
    userCredentialsDTO.password = '';

    this.authService.login( userCredentialsDTO )
    .subscribe(
      res => '',
      err => ''
      );

    // this.saveJWT( '' );
  }

  logout() {
    localStorage.removeItem( Constants.localStorageIds.loggedUserId );
  }

  private saveJWT(jwt: string) {
    localStorage.setItem( Constants.localStorageIds.loggedUserId, jwt );
  }

}
