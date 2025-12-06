import { api } from "./api";
import type { Owner, CreateOwnerRequest } from "../types/owner";

export async function getOwnersList(): Promise<Owner[]> {
  const cachedOwners = sessionStorage.getItem("owners");
  if (cachedOwners) {    
    return JSON.parse(cachedOwners);
  }
  
  const { data } = await api.get("/api/owners");    
  sessionStorage.setItem("owners", JSON.stringify(data)); // Guarda en caché
  
  return data;
}

export async function createOwner(payload: CreateOwnerRequest): Promise<void> {
  sessionStorage.removeItem("owners");
  await api.post("/api/owners", payload);
}