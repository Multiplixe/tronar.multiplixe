import Entry from './entry';
import { EntryStatusEnum, EntryCodeEnum } from './entry.enum';

export class EntryHelper {

    constructor() { }

    static isInvalidStatus(dto: Entry): boolean {
        return dto.status == EntryStatusEnum.invalid;
    }

    static isValidStatus(dto: Entry): boolean {
        return dto.status == EntryStatusEnum.valid;
    }    

    static isInvalidError(dto: Entry): boolean {
        return this.isError(dto, EntryCodeEnum.invalid);
    }    

    static isAlreadyError(dto: Entry): boolean {
        return this.isError(dto, EntryCodeEnum.already);
    }

    static isDifferentCode(dto: Entry): boolean {
        return this.isError(dto, EntryCodeEnum.different);
    }

    private static isError(dto: Entry, code: EntryCodeEnum): boolean {
        return dto.status == EntryStatusEnum.invalid &&
            dto.code == code;
    }

    static resetEntry(dto: Entry) {
        this.set(dto, EntryCodeEnum.none);
    }

    static set(dto: Entry, code: EntryCodeEnum) {
        dto.status = code == EntryCodeEnum.none ? EntryStatusEnum.none : EntryStatusEnum.invalid;
        dto.code = code;
    }


}