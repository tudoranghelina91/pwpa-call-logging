export interface Call {
    callerName: String;
    address: String;
    description: String;
}

export class CallFormModel implements Call {

    constructor(callerName : String, address: String, description: String) {
        this.callerName = callerName,
        this.address = address,
        this.description = description
    }

    callerName : String = '';
    address : String = '';
    description : String = '';
}