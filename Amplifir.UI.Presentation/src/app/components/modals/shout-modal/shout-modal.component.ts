/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

import { Component, OnInit } from '@angular/core';

import { Constants } from '../../../constants';
import { Shout } from '../../../services/apiClient.service';

@Component({
  selector: 'app-shout-modal',
  templateUrl: './shout-modal.component.html',
  styleUrls: ['./shout-modal.component.scss']
})
export class ShoutModalComponent implements OnInit {

  constructor() { }

  shout: Shout = new Shout();

  ngOnInit() {
    this.shout = new Shout( JSON.parse( localStorage.getItem( Constants.localStorageIds.currentShout ) || '' ));
  }

}
