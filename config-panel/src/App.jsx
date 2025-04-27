import { useState } from "react";
import ConfigList from "./components/ConfigList";
import AddConfigForm from "./components/AddConfigForm";
import UpdateConfigForm from "./components/UpdateConfigForm";

function App() {
  const [refreshKey, setRefreshKey] = useState(0);
  const [editingConfig, setEditingConfig] = useState(null);

  const refresh = () => setRefreshKey((k) => k + 1);

  return (
    <div className="max-w-4xl mx-auto mt-10">
      <h1 className="text-3xl font-bold mb-6 text-center">Dynamic Config Panel</h1>

      {editingConfig ? (
        <UpdateConfigForm
          selectedConfig={editingConfig}
          onUpdated={() => {
            refresh();
            setEditingConfig(null);
          }}
          onCancel={() => setEditingConfig(null)}
        />
      ) : (
        <AddConfigForm onAdded={refresh} />
      )}

      <ConfigList
        key={refreshKey}
        onEdit={(config) => setEditingConfig(config)}
        onDeleted={refresh}
      />
    </div>
  );
}

export default App;
