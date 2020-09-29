import { EntryStatusEnum, EntryCodeEnum } from './entry.enum';

export class Entry {

    public name: string;
    public status: EntryStatusEnum;
    public message: string;
    public code: EntryCodeEnum;

    constructor() {
        this.reset();
    }

    hasError() {
        return this.code != 0;
    }

    reset() {
        this.name = '';
        this.status = EntryStatusEnum.none;
        this.message = '';
        this.code = EntryCodeEnum.none;
    }

    setInvalidStatus(code: EntryCodeEnum = EntryCodeEnum.invalid) {
        this.status = EntryStatusEnum.invalid;
        this.code = code;
    }

}

export default Entry;