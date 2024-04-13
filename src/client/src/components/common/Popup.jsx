import React from "react";
import "./Popup.css";

const Popup = ({ message, onCancel, onConfirm }) => {
  return (
    <div className="popup-container">
      <div className="popup">
        <p>{message}</p>
        <div className="popup-buttons">
          <button onClick={onCancel}>Cancel</button>
          <button onClick={onConfirm}>OK</button>
        </div>
      </div>
    </div>
  );
};

export default Popup;
