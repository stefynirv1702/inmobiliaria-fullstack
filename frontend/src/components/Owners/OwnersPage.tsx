import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useState } from "react";
import { getOwnersList } from "../../service/ownersService";
import OwnerForm from "./OwnerForm";
import type { Owner } from "../../types/owner";

export default function OwnersPage() {
    const qc = useQueryClient();
    const [showForm, setShowForm] = useState(false);

    // Paginación
    const [page, setPage] = useState(1);
    const pageSize = 5;

    const { data, isLoading, isError, error } = useQuery<Owner[]>({
        queryKey: ["owners"],
        queryFn: getOwnersList,
    });

    const totalItems = data?.length ?? 0;
    const totalPages = Math.ceil(totalItems / pageSize);

    const paginatedData = data?.slice((page - 1) * pageSize, page * pageSize) ?? [];

    return (
        <div style={{ display: "grid", gridTemplateColumns: "1fr", height: "100%" }}>
            <section style={{ padding: 16, overflow: "auto" }}>
                <div style={{ display: "flex", justifyContent: "space-between", marginBottom: 12 }}>
                    <h2 style={{ color: "#1A4D8F" }}>Owners</h2>
                    <button
                        onClick={() => setShowForm((s) => !s)}
                        style={{
                            padding: "8px 16px",
                            background: "#1A73E8",
                            color: "white",
                            border: "none",
                            borderRadius: 6,
                            cursor: "pointer",
                            boxShadow: "0 2px 6px rgba(0,0,0,0.2)",
                        }}
                    >
                        {showForm ? "Close" : "Add"}
                    </button>
                </div>

                {isLoading && <p>Loading...</p>}
                {isError && <p style={{ color: "red" }}>{(error as Error)?.message ?? "Error"}</p>}

                {!showForm && (
                    <div
                        style={{
                            background: "white",
                            padding: 16,
                            borderRadius: 10,
                            boxShadow: "0 3px 10px rgba(0,0,0,0.12)",
                        }}
                    >
                        <table style={{ width: "100%", borderCollapse: "separate", borderSpacing: 0 }}>
                            <thead>
                                <tr>
                                    <th style={th}>Name</th>
                                    <th style={th}>Address</th>
                                    <th style={th}>Birthday</th>
                                    <th style={th}>Photo</th>
                                </tr>
                            </thead>
                            <tbody>
                                {paginatedData.map((o) => (
                                    <tr
                                        key={o.idOwner ?? `${o.name}-${o.birthday}`}
                                        style={{
                                            ...row,
                                        }}
                                        onMouseEnter={(e) => (e.currentTarget.style.background = "#E8F1FF")}
                                        onMouseLeave={(e) => (e.currentTarget.style.background = "white")}
                                    >
                                        <td style={td}>{o.name}</td>
                                        <td style={td}>{o.address}</td>
                                        <td style={td}>
                                            {o.birthday ? new Date(o.birthday).toLocaleDateString() : "-"}
                                        </td>
                                        <td style={td}>
                                            {o.photo ? (
                                                <a
                                                    href={o.photo}
                                                    target="_blank"
                                                    rel="noreferrer"
                                                    style={{ color: "#1A73E8", textDecoration: "underline" }}
                                                >
                                                    View
                                                </a>
                                            ) : (
                                                "-"
                                            )}
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>

                        {/* PAGINACIÓN */}
                        {totalPages > 1 && (
                            <div
                                style={{
                                    display: "flex",
                                    justifyContent: "center",
                                    gap: 12,
                                    marginTop: 16,
                                }}
                            >
                                <button
                                    onClick={() => setPage((p) => Math.max(1, p - 1))}
                                    disabled={page === 1}
                                    style={paginationButton(page === 1)}
                                >
                                    Prev
                                </button>

                                <span style={{ fontWeight: "bold", color: "#1A4D8F" }}>
                                    Page {page} / {totalPages}
                                </span>

                                <button
                                    onClick={() => setPage((p) => Math.min(totalPages, p + 1))}
                                    disabled={page === totalPages}
                                    style={paginationButton(page === totalPages)}
                                >
                                    Next
                                </button>
                            </div>
                        )}
                    </div>
                )}
            </section>

            {showForm && (
                <section
                    style={{
                        borderLeft: "1px solid #eee",
                        padding: 16,
                        overflow: "auto",
                        background: "white",
                        borderRadius: "8px",
                    }}
                >
                    <OwnerForm
                        onSuccess={async () => {
                            await qc.invalidateQueries({ queryKey: ["owners"] });
                            setShowForm(false);
                        }}
                    />
                </section>
            )}
        </div>
    );
}

// ESTILOS -------------------------------------------------------------------

const th: React.CSSProperties = {
    textAlign: "left",
    padding: 12,
    background: "#DCE8FF",
    borderBottom: "2px solid #A8C1FF",
    color: "#1A4D8F",
    fontWeight: "bold",
};

const td: React.CSSProperties = {
    padding: 12,
    borderBottom: "1px solid #eee",
};

const row: React.CSSProperties = {
    background: "white",
    transition: "0.2s ease",
};

const paginationButton = (disabled: boolean): React.CSSProperties => ({
    padding: "6px 14px",
    background: disabled ? "#BFD4FF" : "#1A73E8",
    color: "white",
    border: "none",
    borderRadius: 6,
    cursor: disabled ? "not-allowed" : "pointer",
    boxShadow: disabled ? "none" : "0 2px 6px rgba(0,0,0,0.15)",
    transition: "0.2s",
});