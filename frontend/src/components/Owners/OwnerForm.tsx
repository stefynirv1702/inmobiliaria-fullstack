import { useForm } from "react-hook-form";

import { z } from "zod";

import { zodResolver } from "@hookform/resolvers/zod";

import { useMutation } from "@tanstack/react-query";

import { createOwner } from "../../service/ownersService";



const schema = z.object({

  name: z.string().min(2, "Short name"),

  address: z.string().min(3, "Invalid address"),

  photo: z.string().url("Must be a valid URL").or(z.literal("").transform(() => "")),

  birthday: z.string().min(1, "Date required"),

});



type FormValues = z.infer<typeof schema>;



export default function OwnerForm({ onSuccess }: { onSuccess: () => void }) {

  const { register, handleSubmit, formState: { errors, isSubmitting } } = useForm<FormValues>({

    resolver: zodResolver(schema),

  });



  const mutation = useMutation({

    mutationFn: createOwner,

    onSuccess: () => onSuccess(),

  });



  const onSubmit = (values: FormValues) => {

    const iso = new Date(values.birthday).toISOString();

    mutation.mutate({ ...values, birthday: iso });

  };



  const err: React.CSSProperties = { color: "crimson", fontSize: 13, marginTop: 4 };



  return (

    <form

      onSubmit={handleSubmit(onSubmit)}

      style={{

        display: "grid",

        gap: 18,

        maxWidth: 380,

        margin: "0 auto",

        padding: 20,

        background: "#fff",

        borderRadius: 12,

        boxShadow: "0 4px 12px rgba(0,0,0,0.08)",

      }}

    >

      <h3 style={{ margin: 0, textAlign: "center" }}>New Owner</h3>



      <label style={{ display: "grid", gap: 6 }}>

        <span>Name</span>

        <input

          type="text"

          {...register("name")}

          style={{

            padding: "8px 10px",

            borderRadius: 6,

            border: "1px solid #ccc",

          }}

        />

        {errors.name && <span style={err}>{errors.name.message}</span>}

      </label>



      <label style={{ display: "grid", gap: 6 }}>

        <span>Address</span>

        <input

          type="text"

          {...register("address")}

          style={{

            padding: "8px 10px",

            borderRadius: 6,

            border: "1px solid #ccc",

          }}

        />

        {errors.address && <span style={err}>{errors.address.message}</span>}

      </label>



      <label style={{ display: "grid", gap: 6 }}>

        <span>Photo (URL)</span>

        <input

          type="text"

          {...register("photo")}

          style={{

            padding: "8px 10px",

            borderRadius: 6,

            border: "1px solid #ccc",

          }}

        />

        {errors.photo && <span style={err}>{errors.photo.message}</span>}

      </label>



      <label style={{ display: "grid", gap: 6 }}>

        <span>Birthday</span>

        <input

          type="date"

          {...register("birthday")}

          style={{

            padding: "8px 10px",

            borderRadius: 6,

            border: "1px solid #ccc",

          }}

        />

        {errors.birthday && <span style={err}>{errors.birthday.message}</span>}

      </label>



      <button

        type="submit"

        disabled={isSubmitting || mutation.isPending}

        style={{

          padding: "10px 14px",

          borderRadius: 6,

          border: "none",

          background: mutation.isPending ? "#666" : "#007bff",

          color: "White",

          cursor: mutation.isPending ? "not-allowed" : "pointer",

          fontWeight: 500,

          transition: "0.2s",

        }}

      >

        {mutation.isPending ? "Saving…" : "Save Owner"}

      </button>



      {mutation.isError && (

        <p style={{ color: "red", margin: 0 }}>

          {(mutation.error as Error)?.message ?? "Error saving owner"}

        </p>

      )}

    </form>

  );

}