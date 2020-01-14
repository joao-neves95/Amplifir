/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

export class RouterLinkViewModel {

  constructor( label: string, url: string ) {
    this.label = label;
    this.url = url;
  }

  label: string = '';
  url: string = '';

}
