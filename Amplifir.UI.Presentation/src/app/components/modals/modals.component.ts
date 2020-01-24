/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

import { Component, AfterViewInit, Input } from '@angular/core';

import { Constants } from '../../constants';
import { ModalType } from '../../enums/modalType';

/**
 * ATENTION: Do not create multiple modals. There can only be one.
 */
@Component({
  selector: 'app-modals',
  templateUrl: './modals.component.html',
  styleUrls: ['./modals.component.scss']
})
export class ModalsComponent implements AfterViewInit {

  constructor() {
    ModalsComponent._ = this;

    this.id = ModalsComponent.ID;
    this.activeModal = ModalType.AuthModal;
    this.title = Constants.defaultLabels.authModalTitle;
    this.goBtnLabel = Constants.defaultLabels.authModalTitle;
  }

  /** The current ModalsComponent instance. */
  public static _: ModalsComponent;

  public static readonly ID: string = Constants.ids.modalsComponent;
  public readonly id: string;
  public activeModal: ModalType;

  @Input() public title: string;
  @Input() public goBtnLabel: string;

  // #region STATIC METHODS

  static open( modalType: ModalType ): void {
    $( '#' + ModalsComponent.ID ).modal( 'show' );
    ModalsComponent._.activeModal = modalType;

    switch (modalType) {
      case ModalType.AuthModal:
        ModalsComponent._.title = Constants.defaultLabels.authModalTitle;
        ModalsComponent._.goBtnLabel = Constants.defaultLabels.authModalTitle;
        break;

      case ModalType.ShoutModal:
        ModalsComponent._.title = Constants.defaultLabels.shoutModalTitle;
        break;

      default:
        break;
    }
  }

  static close( modalType: ModalType ): void {
    $( '#' + ModalsComponent.ID ).modal( 'hide' );
    ModalsComponent._.activeModal = ModalType.None;
  }

  // #endregion STATIC METHODS

  ngAfterViewInit() {
    if (!Constants.booleans.showAuthModal) {
      return;
    }

    setTimeout(() => {
      $( '#' + this.id ).modal({
        show: true,
        keyboard: true
      });
    }, Constants.timings.authModalInitTime );

  }

}
