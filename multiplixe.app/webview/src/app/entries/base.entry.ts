
import { Entry } from './entry';

export class BaseEntry<T> extends Entry {

    public field: boolean = true;

    constructor(public value: T) {
        super()
    }

}
