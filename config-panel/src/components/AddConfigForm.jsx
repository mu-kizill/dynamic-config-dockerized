import { useState } from "react";

const API_URL = `${import.meta.env.VITE_API_BASE_URL}/configurations`;

function AddConfigForm({ onAdded }) {
  const [formData, setFormData] = useState({
    name: "",
    type: "string",
    value: "",
    isActive: true,
    applicationName: "SERVICE-A",
  });

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    let parsedValue;
    switch (formData.type) {
      case "int":
        parsedValue = parseInt(formData.value);
        break;
      case "double":
        parsedValue = parseFloat(formData.value);
        break;
      case "bool":
        parsedValue = formData.value === "true" || formData.value === true;
        break;
      default:
        parsedValue = formData.value;
    }

    const payload = { ...formData, value: String(parsedValue) };

    fetch(API_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    })
      .then((res) => {
        if (!res.ok) throw new Error("Kayıt eklenemedi");
        return res.json();
      })
      .then(() => {
        onAdded?.();
        setFormData({
          name: "",
          type: "string",
          value: "",
          isActive: true,
          applicationName: "SERVICE-A",
        });
      })
      .catch((err) => alert(err.message));
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4 mb-6 p-6 border rounded shadow">
      <h2 className="text-xl font-bold">Yeni Konfigürasyon Ekle</h2>

      <input
        name="name"
        value={formData.name}
        onChange={handleChange}
        placeholder="Key (name)"
        required
        className="w-full border p-2"
      />

      <select name="type" value={formData.type} onChange={handleChange} className="w-full border p-2">
        <option value="string">string</option>
        <option value="int">int</option>
        <option value="bool">bool</option>
        <option value="double">double</option>
      </select>

      <input
        name="value"
        value={formData.value}
        onChange={handleChange}
        placeholder="Değer"
        required
        className="w-full border p-2"
      />

      <label className="flex items-center gap-2">
        <input
          type="checkbox"
          name="isActive"
          checked={formData.isActive}
          onChange={handleChange}
        />
        Aktif mi?
      </label>

      <input
        name="applicationName"
        value={formData.applicationName}
        onChange={handleChange}
        placeholder="Application Name"
        required
        className="w-full border p-2"
      />

      <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded">
        Kaydet
      </button>
    </form>
  );
}

export default AddConfigForm;
