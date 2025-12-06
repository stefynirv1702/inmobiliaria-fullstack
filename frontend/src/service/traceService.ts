import { api } from "./api";
import type { CreateTraceRequest} from "../types/trace";

export async function createTrace(payload: CreateTraceRequest): Promise<void> {  
  await api.post("/api/trace", payload);
}
