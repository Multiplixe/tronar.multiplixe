
export enum CultureEnum {
    en = 'en',
    pt = 'pt'
}

class CultureEnumHelper {

    private static items: Array<CultureEnum> = [
        CultureEnum.en,
        CultureEnum.pt
    ];

    static cultures(): CultureEnum[] {
        return this.items;
    }

    static culture(): CultureEnum {
        return CultureEnum.pt;
    }

    static CultureEnum() {
        return this.culture().toString();
    }

    static contains(l: any): boolean {
        let index = this.items.indexOf(<CultureEnum>l);
        return index >= 0 
    }

}


export default CultureEnumHelper;

