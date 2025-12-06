export type Property = {
    idProperty?: string;
    name: string;
    address: string;
    price: number;
    codeInternal: string;
    year: number;
    idOwner: string;
    image: string;
};

export type PropertyDetail = {
    idProperty?: string;
    ownerId: string;
    name: string;
    address: string;
    price: number;
    codeInternal: string;
    year: number;
    image: string;
    trace: Trace[];
};

export type Trace = {
    idTrace: string;
    dateSale: string;
    name: string;
    value: number;
    tax: number;
}

export type CreatePropertyRequest = {
    name: string;
    address: string;
    price: number;
    year: number;
    image: string;
    idOwner: string;
};

export type GetPropertyRequest = {
    idProperty: string;
};