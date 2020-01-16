/*
 * Copyright (c) 2019 - 2020 Jo�o Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

import { Component, OnInit } from '@angular/core';

import { Constants } from '../../../shared/constants';

@Component({
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor() { }

  readonly CONSTANTS = Constants;

  userName: string = Constants.defaultLabels.userName;
  bio: string = '';
  location: string = '';
  birthDate: string = '';
  website: string = '';
  followingCount: number = 0;
  followersCount: number = 0;

  shouts: string[] = [];

  ngOnInit() {
  }

}
