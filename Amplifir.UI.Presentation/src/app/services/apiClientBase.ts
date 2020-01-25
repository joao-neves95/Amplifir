
export class ApiClientBase {

  constructor() {}

  protected transformOptions( options: any ) {
    if ( options && options.headers ) {
      options.headers = ( options.headers as Headers ).set( 'Authorization', 'TEST' );
    }

    return Promise.resolve( options );
  }

}
