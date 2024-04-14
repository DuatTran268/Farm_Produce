import React from "react";
import "./Popup.css";

const Popup = ({ message, onCancel, onConfirm }) => {
  return (
    <div className="popup-container">
      <div className="popup">
        <p className="messgae_popup">{message}</p>
        <div className="popup-buttons">
          <button className="btn_action btn_cancel" onClick={onCancel}>Cancel</button>
          <button className="btn_action btn_oke" onClick={onConfirm}>OK</button>
        </div>
      </div>
    </div>
  );
};

export default Popup;
