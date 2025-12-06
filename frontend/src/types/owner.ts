export type Owner = {
  idOwner?: string;           
  name: string;
  address: string;
  photo: string;
  birthday: string;       
};

export type OwnerList = {
  idOwner: string;
  name: string;
};

export type CreateOwnerRequest = {
  name: string;
  address: string;
  photo: string;
  birthday: string;       
};