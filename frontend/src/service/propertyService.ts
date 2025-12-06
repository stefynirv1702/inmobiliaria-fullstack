import { api } from "./api";
import type { Property, CreatePropertyRequest, PropertyDetail } from "../types/property";

export async function getProperties(): Promise<Property[]> {
  const { data } = await api.get("/api/property");
  return data;
}

export async function createProperty(payload: CreatePropertyRequest): Promise<void> {  
  await api.post("/api/property", payload);
}

export async function getPropertyById(id: string): Promise<PropertyDetail> {
    const { data } = await api.get(`/api/property/${id}`);
    console.log(data);    
    return data;
};