import { useEffect, useState } from "react";
import axios from "axios";
import { ResizableBox } from "react-resizable";
import "react-resizable/css/styles.css";

const BASE_URL = import.meta.env.VITE_BASE_URL;
let abortController: AbortController | undefined;

const App = () => {
  const [dimensions, setDimensions] = useState({ width: 0, height: 0 });
  const [error, setError] = useState("");
  const [isValidating, setIsValidating] = useState(false);

  useEffect(() => {
    axios
      .get(`${BASE_URL}api/Rectangle/GetRectangle`)
      .then((response) => setDimensions(response.data));
  }, []);

  const handleResizeStop = async () => {
    let requestCancelled = false;
    setIsValidating(true);
    setError("");

    if (abortController) {
      abortController.abort();
    }

    abortController = new AbortController();

    await axios
      .patch(`${BASE_URL}api/Rectangle/UpdateRectangle`, dimensions, {
        signal: abortController.signal,
      })
      .catch((err) => {
        if (axios.isCancel(err)) {
          requestCancelled = true;
        } else if (err?.response?.data?.message) {
          setError(err.response.data.message);
        } else {
          setError("Something wrong.");
        }
      })
      .finally(() => {
        if (!requestCancelled) {
          setIsValidating(false);
        }
      });
  };

  const handleResize = (_: any, data: any) => {
    setDimensions({
      width: data.size.width,
      height: data.size.height,
    });
  };

  const perimeter = 2 * (dimensions.width + dimensions.height);

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
        width: "100vw",
        fontFamily: "Arial, sans-serif",
        margin: 0,
      }}
    >
      <div
        style={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          textAlign: "center",
        }}
      >
        <ResizableBox
          width={dimensions.width}
          height={dimensions.height}
          resizeHandles={["se"]}
          onResize={handleResize}
          onResizeStop={handleResizeStop}
          minConstraints={[49, 50]}
          maxConstraints={[499, 500]}
          style={{
            border: "1px solid black",
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
          }}
        >
          <div
            style={{
              width: "100%",
              height: "100%",
              backgroundColor: "lightblue",
            }}
          ></div>
        </ResizableBox>

        <p>Perimeter: {perimeter} units</p>
        {isValidating && <p>Validating...</p>}
        {error && <p style={{ color: "red" }}>Error: {error}</p>}
      </div>
    </div>
  );
};

export default App;
