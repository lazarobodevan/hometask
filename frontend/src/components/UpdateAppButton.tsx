import { useEffect, useState } from 'react';
import { Snackbar, Button, Alert } from '@mui/material';
import { updateSW } from '../main';

const UpdateAppButton = () => {
  const [show, setShow] = useState(false);

  useEffect(() => {
    const handler = () => setShow(true);
    window.addEventListener('sw-update-available', handler);

    return () => {
      window.removeEventListener('sw-update-available', handler);
    };
  }, []);

  const handleUpdate = async () => {
    await updateSW(true); // força atualização
    window.location.reload();
  };

  return (
    <Snackbar open={show} anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}>
      <Alert
        severity="info"
        action={
          <Button color="inherit" size="small" onClick={handleUpdate}>
            Atualizar
          </Button>
        }
      >
        Nova versão disponível 🚀
      </Alert>
    </Snackbar>
  );
};

export default UpdateAppButton;