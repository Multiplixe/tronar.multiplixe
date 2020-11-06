import { BaseEntry } from './base.entry'

export class NumberEntry extends BaseEntry<number>  {

    constructor(value: number) {
        super(value);
    }

}