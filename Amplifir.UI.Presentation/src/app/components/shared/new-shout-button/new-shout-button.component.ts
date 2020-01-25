import { Component, OnInit } from '@angular/core';

import { ModalType } from 'src/app/enums/modalType';
import { ModalsComponent } from '../../modals/modals.component';

@Component({
  selector: 'app-new-shout-button',
  templateUrl: './new-shout-button.component.html',
  styleUrls: ['./new-shout-button.component.scss']
})
export class NewShoutButtonComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  openNewShoutModal() {
    ModalsComponent.open( ModalType.NewShoutModal );
  }

}
