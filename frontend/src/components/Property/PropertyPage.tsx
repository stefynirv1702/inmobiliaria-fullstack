import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { getProperties } from "../../service/propertyService";
import PropertyForm from "./PropertyForm";
import type { Property } from "../../types/property";
import type { OwnerList } from "../../types/owner";

export default function PropertyPage() {
    const qc = useQueryClient();
    const navigate = useNavigate();
    const [showForm, setShowForm] = useState(false);    
    const cachedOwners = sessionStorage.getItem("owners");

    const { data, isLoading, isError, error } = useQuery<Property[]>({
        queryKey: ["property"],
        queryFn: getProperties,
    });

    const getOwnerName = (idToFind : string) => {            
        if (cachedOwners) {    
            const itemList: OwnerList[] = JSON.parse(cachedOwners);
            const foundItem = itemList.find(item => item.idOwner === idToFind);
            return foundItem?.name;
        }
    }    

    const onViewProperty = (id: string) => {
    navigate(`/properties/${id}`);
    };

    return (
        <div style={{ display: "grid", gridTemplateColumns: showForm ? "1fr 380px" : "1fr", gap: 20, padding: 20 }}>
            
            {/* LISTA */}
            <section style={{ overflow: "auto" }}>
                <div style={{ display: "flex", justifyContent: "space-between", marginBottom: 16 }}>
                    <h2 style={{ margin: 0, color: "#1A365D" }}>Properties</h2>

                    <button
                        onClick={() => setShowForm((s) => !s)}
                        style={{
                            padding: "8px 18px",
                            background: showForm ? "#2C5282" : "#3182CE",
                            color: "white",
                            border: "none",
                            borderRadius: 8,
                            cursor: "pointer",
                            fontWeight: 600,
                            transition: "0.2s",
                            boxShadow: "0 2px 6px rgba(0,0,0,0.15)"
                        }}
                    >
                        {showForm ? "Close" : "Add"}
                    </button>
                </div>

                {isLoading && <p>Loading...</p>}
                {isError && <p style={{ color: "red" }}>{(error as Error)?.message ?? "Error"}</p>}

                {!showForm && (
                    <div style={{
                        display: "grid",
                        gridTemplateColumns: "repeat(auto-fill, minmax(320px, 1fr))",
                        gap: 20
                    }}>
                        {data?.map((o) => (
                            <div
                                key={o.idProperty}
                                style={{
                                    background: "white",
                                    borderRadius: 12,
                                    padding: 16,
                                    boxShadow: "0 4px 12px rgba(0,0,0,0.08)",
                                    border: "1px solid #E2E8F0"
                                }}
                            >
                                <h3 style={{ marginTop: 0, color: "#2B6CB0" }}>{o.name}</h3>
                                <p><strong>Address:</strong> {o.address}</p>
                                <p><strong>Price:</strong> {o.price.toLocaleString("en-US", { style: "currency", currency: "USD" })}</p>
                                <p><strong>Code:</strong> {o.codeInternal}</p>
                                <p><strong>Year:</strong> {o.year}</p>
                                <p><strong>Owner:</strong> {getOwnerName(o.idOwner)}</p>

                                <button
                                    onClick={() => onViewProperty(o.idProperty!)}
                                    style={{
                                        marginTop: 10,
                                        width: "100%",
                                        padding: "10px 0",
                                        background: "#3182CE",
                                        color: "white",
                                        border: "none",
                                        borderRadius: 8,
                                        cursor: "pointer",
                                        fontWeight: 600,
                                        transition: "0.2s",
                                        boxShadow: "0 2px 6px rgba(0,0,0,0.15)"
                                    }}
                                >
                                    View Property
                                </button>
                            </div>
                        ))}
                    </div>
                )}
            </section>

            {/* FORM */}
            {showForm && (
                <section style={{
                    borderLeft: "1px solid #E2E8F0",
                    padding: 16,
                    overflow: "auto",
                    background: "white",
                    borderRadius: 12,
                    boxShadow: "0 4px 12px rgba(0,0,0,0.08)"
                }}>
                    <PropertyForm
                        onSuccess={async () => {
                            await qc.invalidateQueries({ queryKey: ["property"] });
                            setShowForm(false);
                        }}
                    />
                </section>
            )}
        </div>
    );
}