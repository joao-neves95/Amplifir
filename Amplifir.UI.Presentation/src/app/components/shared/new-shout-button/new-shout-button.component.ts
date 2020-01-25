import {
  Component,
  OnInit
} from '@angular/core';

import {
  ModalType
} from 'src/app/enums/modalType';
import {
  ModalsComponent
} from '../../modals/modals.component';
import {
  Constants
} from '../../../constants';

@Component( {
  selector: 'app-new-shout-button',
  templateUrl: './new-shout-button.component.html',
  styleUrls: [ './new-shout-button.component.scss' ]
} )
export class NewShoutButtonComponent implements OnInit {

  constructor() {}

  ngOnInit() {}

  openNewShoutModal() {
    if ( !localStorage.getItem( Constants.localStorageIds.loggedUserId ) ) {
      alert( 'You must login to shout!' );
      setTimeout( () => ModalsComponent.open( ModalType.AuthModal ), 500 );
      return false;
    }

    ModalsComponent.open( ModalType.NewShoutModal );
  }

}
