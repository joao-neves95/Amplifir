import {
  Component,
  OnInit,
  Input
} from '@angular/core';

import {
  Constants
} from '../../../constants';
import {
  ModalType
} from 'src/app/enums/modalType';
import {
  ModalsComponent
} from '../../modals/modals.component';
import {
  Shout,
  ShoutsService,
  ApiException,
  ApiResponseOfCreateReactionResult
} from '../../../services/apiClient.service';
import {
  ApiClientHandlers
} from 'src/app/services/apiClientHandlers';

@Component( {
  selector: 'app-shout-card',
  templateUrl: './shout-card.component.html',
  styleUrls: [ './shout-card.component.scss' ]
} )
export class ShoutCardComponent implements OnInit {

  // tslint:disable-next-line: no-shadowed-variable
  constructor( private shoutsService: ShoutsService ) {}

  @Input() clickable: boolean = true;
  @Input() shout: Shout = new Shout();
  createDate: string = new Date().toLocaleString();

  canceledClasses: string = '';

  ngOnInit() {
    if ( !this.clickable ) {
      this.canceledClasses += 'cancel-clickable';
    }

    this.createDate = this.shout.createDate.toLocaleString();
  }

  openShoutModal( e: MouseEvent ) {
    if ( !this.clickable || $( e.target as HTMLButtonElement ).parent( '.reaction-count' ).length > 0 ) {
      e.preventDefault();
      return false;
    }

    localStorage.setItem( Constants.localStorageIds.currentShout, JSON.stringify( this.shout ) );
    ModalsComponent.open( ModalType.ShoutModal );
  }

  likeShout() {
    this.shoutsService.postLike( this.shout.id )
      .subscribe(
        res => this.handlePostReaction( res ),
        err => this.handlePostReactionError( err, true )
      );
  }

  dislikeShout() {
    this.shoutsService.postDislike( this.shout.id )
      .subscribe(
        res => this.handlePostReaction( res ),
        err => this.handlePostReactionError( err, false )
      );
  }

  private handlePostReaction( res: ApiResponseOfCreateReactionResult ) {
    if ( res.message ) {
      alert( res.message );
    }
  }

  private handlePostReactionError( err: ApiException, isLike: boolean ) {
    if ( ApiClientHandlers.handle500Status( err ) ) {
      return false;
    }

    if ( ApiClientHandlers.handle401Status( err, isLike ? 'like a shout.' : 'dislike a shout.' ) ) {
      return false;
    }
  }

}
