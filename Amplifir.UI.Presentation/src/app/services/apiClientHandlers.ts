import {
  ApiException
} from './apiClient.service';

export class ApiClientHandlers {

  constructor() {
    throw new Error( 'You can not instantiate the static class "ApiClientHandlers"' );
  }

  /**
   * Returns true if it handled an authentication error, or false otherwise.
   *
   * @param err
   * @param action
   */
  static handle401Status( err: ApiException, action: string ): boolean {
    if ( ( err as ApiException ).status === 401 ) {
      alert( 'You must login to ' + action );
      return true;
    }

    return false;
  }

  /**
   * Returns true if it handled an internal server error, or false otherwise.
   *
   * @param err
   * @param message
   */
  static handle500Status( err: ApiException, message: string = 'Unkown Error.' ): boolean {
    if ( ( err as ApiException ).status === 500 ) {
      alert( message );
      return true;
    }

    return false;
  }

}
