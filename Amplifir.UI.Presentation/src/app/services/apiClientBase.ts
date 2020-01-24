
export class ApiClientBase {

  constructor() {}

  protected transformOptions( options: any ) {
    if ( options && options.headers ) {
      ( <Headers> options.headers ).set( 'Authorization', 'TEST' );
    }

    return Promise.resolve( options );
  }

}
