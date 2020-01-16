/*
 * Copyright (c) 2019 - 2020 Jo�o Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

export class RouterLinkViewModel {

  constructor( label: string, url: string, icon: string ) {
    this.label = label;
    this.url = url;
    this.icon = icon;
  }

  label: string = '';
  url: string = '';
  icon: string = '';

}
