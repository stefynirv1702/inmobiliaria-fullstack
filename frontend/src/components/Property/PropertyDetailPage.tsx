import { useParams, useNavigate } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import type { PropertyDetail } from "../../types/property";
import { getPropertyById } from "../../service/propertyService";

export default function PropertyDetailPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const { data, isLoading, isError } = useQuery<PropertyDetail>({
    queryKey: ["property", id],
    queryFn: () => getPropertyById(id!),
    enabled: !!id,
  });
  
  if (isLoading) return <p>Loading property...</p>;
  if (isError) return <p style={{ color: "red" }}>Error loading property</p>;
  
  return (
    <div style={{ padding: 16 }}>
      <h2>{data?.name}</h2>
      <img
        src={
            data?.image && data.image.trim() !== ""
            ? data.image
            : "https://thumbs.dreamstime.com/b/default-image-icon-vector-missing-picture-page-website-design-mobile-app-no-photo-available-236105299.jpg"
        }
        alt={data?.name}
        style={{ width: "400px", borderRadius: 8, marginBottom: 16 }}
      />

        <div style={{ marginBottom: 16 }}>
        <button onClick={() => navigate("/properties")}style={btn}>⬅ Back to Properties</button>
        <button onClick={() => navigate(`/trace/${id}`)} style={{ ...btn, marginLeft: 8 }}>➕ Add Trace</button>
      </div>

      <h3>Trace</h3>
      <table style={{ borderCollapse: "collapse", width: "100%" }}>
        <thead>
          <tr>
            <th style={th}>Date Sale</th>
            <th style={th}>Name</th>
            <th style={th}>Value</th>            
            <th style={th}>Tax</th>
          </tr>
        </thead>
        <tbody>
            
          {data?.trace?.map((t) => (
            <tr key={t.idTrace} style={{ borderTop: "1px solid #eee" }}>
                <td style={td}>{new Date(t.dateSale).toLocaleDateString()}</td>                                
                <td style={td}>{t.name}</td>
                <td style={td}>{t.value}</td>                
                <td style={td}>{t.tax}</td>
            </tr>
            ))}
        </tbody>
      </table>
    </div>
  );
}

const th: React.CSSProperties = { textAlign: "left", padding: 8, background: "#fafafa", borderBottom: "1px solid #eee" };
const td: React.CSSProperties = { padding: 8 };
const btn: React.CSSProperties = {
  background: "#1976d2",
  color: "white",
  border: "none",
  padding: "8px 12px",
  borderRadius: 4,
  cursor: "pointer",
};

