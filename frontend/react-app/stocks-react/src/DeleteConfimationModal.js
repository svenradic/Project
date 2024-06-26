import React from 'react'
import './delete.model.css';

function DeleteConfimationModal({ show, handleClose, handleDelete, index }) {
  if (!show) {
    return null;
  }

  return (
    <div className='modalOverlay'>
      <div className='modal'>
        <div className='modalHeader'>
          <h2>Confirm Delete</h2>
          <button onClick={handleClose} className='closeButton'>&times;</button>
        </div>
        <div className='modalBody'>
          <p>Are you sure you want to delete this item?</p>
        </div>
        <div className='modalFooter'>
          <button onClick={handleClose} className='cancelButton'>Cancel</button>
          <button onClick={()=> handleDelete(index)} className='deleteButton'>Delete</button>
        </div>
      </div>
    </div>
  );
}

export default DeleteConfimationModal
